import os
import sys

oProjName = sys.argv[1]
oDSYMDirpath = sys.argv[2]

oPlistFilepath = f"../../{oProjName}/Assets/Firebase/GoogleService-Info.plist"
oDSYMUploaderPath = f"../../{oProjName}/Builds/iOS/Pods/FirebaseCrashlytics/upload-symbols"

os.system(f"{oDSYMUploaderPath} -gsp {oPlistFilepath} -p ios {oDSYMDirpath}")
