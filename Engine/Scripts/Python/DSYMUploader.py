import os
import sys

oProjName = sys.argv[1]
oDSYMDirPath = sys.argv[2]

oPlistFilePath = f"../../{oProjName}/Assets/Firebase/GoogleService-Info.plist"
oDSYMUploaderPath = f"../../{oProjName}/Builds/iOS/Pods/FirebaseCrashlytics/upload-symbols"

os.system(f"{oDSYMUploaderPath} -gsp \"{oPlistFilePath}\" -p ios \"{oDSYMDirPath}\"")
