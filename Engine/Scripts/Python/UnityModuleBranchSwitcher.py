import os
import sys

oProjName = sys.argv[1]
oBranchName = sys.argv[2]
oOriginBranchName = sys.argv[3]

# 원본 브랜치 이름이 유효 할 경우
if len(oOriginBranchName) >= 1:
	os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git checkout -b {oBranchName} {oOriginBranchName}\"")
else:
	os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git checkout {oBranchName}\"")
