//
//  KGDefine.hpp
//  E01
//
//  Created by 이상동 on 2021/10/06.
//

#ifndef KGDefine_hpp
#define KGDefine_hpp

#include <iostream>
#include <cassert>
#include <memory>

#define SAFE_FREE(TARGET)				if((TARGET) != nullptr) { free((TARGET)); (TARGET) = nullptr; }

#endif /* KGDefine_hpp */
