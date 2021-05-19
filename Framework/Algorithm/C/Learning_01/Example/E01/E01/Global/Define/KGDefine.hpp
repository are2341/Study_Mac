//
//  KGDefine.hpp
//  E01
//
//  Created by 이상동 on 2021/04/12.
//

#ifndef KGDefine_hpp
#define KGDefine_hpp

#include <stdio.h>
#include <assert.h>
#include <limits.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define SAFE_FREE(TARGET)			if((TARGET) != nullptr) { free((TARGET)); (TARGET) = nullptr; }
#define SAFE_FCLOSE(TARGET)			if((TARGET) != nullptr) { fclose((TARGET)); (TARGET) = nullptr; }

#endif /* KGDefine_hpp */
