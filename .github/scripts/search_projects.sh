#!/bin/bash

results=""
declare -A processed_projects
declare -A project_references_cache

# ディレクトリを再帰的に検索して、 .csproj ファイルを探す
function search_projects() {
  local dir=$1
  local csproj_files=$(find $dir -name "*.csproj")
  for csproj_file in $csproj_files; do
    # results が空でない場合、改行を追加
    if [ -n "$results" ]; then
      results+="\n"
    fi

    results+="$csproj_file:"
    results+="\n  - '$(dirname "$csproj_file")/**'"

    project_references=()
    process_csproj_file "$csproj_file"
    for reference in "${project_references[@]}"; do
      results+="\n  - '$(dirname "$reference")/**'"
    done
  done
}

# .csproj ファイルを解析して ProjectReference を取得
function process_csproj_file() {
  local csproj_file=$1
  # 既に処理された .csproj ファイルはスキップ
  if [[ -n "${processed_projects[$csproj_file]}" ]]; then
    local cached_references=("${project_references_cache[$csproj_file]}")
    IFS=' ' read -r -a cached_references <<< "${project_references_cache[$csproj_file]}"
    project_references+=("${cached_references[@]}")
    return
  fi

  local csproj_dir=$(dirname "$csproj_file")

  local references=$(grep -oP '<ProjectReference Include="[^"]*"' "$csproj_file" | sed -e 's/<ProjectReference Include="//' -e 's/"//')
  local current_references=()
  for reference in $references; do
    # \ を / に変換
    reference=$(echo "$reference" | sed -e 's/\\/\//g')
    # reference は $csproj_file に対しての相対パスなので、ルートディレクトリからの相対パスに変換
    reference=./$(realpath --relative-to=. "$csproj_dir/$reference")
    current_references+=($reference)
    # 再帰的に参照プロジェクトを解析
    process_csproj_file "$reference"
  done

  # キャッシュに保存
  project_references_cache["$csproj_file"]="${current_references[@]}"
  project_references+=("${current_references[@]}")

  # 処理済みとしてマーク
  processed_projects["$csproj_file"]=1
}

search_projects .

echo -e "$results"
