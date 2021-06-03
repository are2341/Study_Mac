import os
import sys

oProjName = sys.argv[1]

oProjPath = f"../../../../../../\"{oProjName}\""
oOutputPath = f"../../../../../../\"{oProjName}\"/Assets/01.UnityProject/00.AutoCreate/Scripts/Runtime/Global/Utility/External/MessagePack/CMsgPackResolver.cs"

oConditionalSymbol = "MSG_PACK_ENABLE,USE_CUSTOM_PROJ_OPTS"
os.system(f"./MsgPackCompiler -i \"{oProjPath}\" -o \"{oOutputPath}\" -c \"{oConditionalSymbol}\" -ms \"MSG_PACK_ENABLE\"")
