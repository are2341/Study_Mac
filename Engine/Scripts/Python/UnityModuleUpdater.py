import os
import sys

oProjName = sys.argv[1]

os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git fetch --tags --force\"")
os.system(f"python3 UnityModuleCmdExecuter.py \"{oProjName}\" \"git pull -p\"")
