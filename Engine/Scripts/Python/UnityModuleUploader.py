import os
import sys

oProjName = sys.argv[1]
oCommitMsg = sys.argv[2]
oBranchName = sys.argv[3]

os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git add .\"")
os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git commit -m \'{oCommitMsg}\'\"")

# 브랜치 이름이 유효 할 경우
if len(oBranchName) >= 1:
	os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git push origin -u {oBranchName}\"")
else:
	os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git push\"")
