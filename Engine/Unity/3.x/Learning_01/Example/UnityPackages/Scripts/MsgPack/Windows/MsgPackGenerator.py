import os
import sys

oProjName = sys.argv[1]

oProjPath = f"../../../../../\"{oProjName}\""
oOutputPath = f"../../../../../\"{oProjName}\"/Assets/01.SubAutoCreate/Scripts/Runtime/Global/Utility/External/MessagePack/CMsgPackResolver.cs"

oConditionalSymbol = "MSG_PACK_ENABLE,USE_AUTO_CREATE"
os.system(f"dotnet mpc -i \"{oProjPath}\" -o \"{oOutputPath}\" -c \"{oConditionalSymbol}\" -ms \"MSG_PACK_ENABLE\"")
