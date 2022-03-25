import os
import sys

oProjName = sys.argv[1]
oTagName = sys.argv[2]
oReplaceTagName = sys.argv[3]

os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git tag -d {oTagName}\"")
os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git push origin --delete {oTagName}\"")
os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git tag {oReplaceTagName}\"")
os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git push origin --tags\"")
