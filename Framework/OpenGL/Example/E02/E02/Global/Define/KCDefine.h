//
//  KCDefine.h
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#pragma once
#import "KGDefine.h"

#include <iostream>
#include <cassert>
#include <filesystem>

#define DECLARE_GETTER(DATA_TYPE, NAME, VAR_NAME)					DATA_TYPE Get##NAME(void) { return VAR_NAME; }
#define DECLARE_SETTER(DATA_TYPE, NAME, VAR_NAME)					void Set##NAME(DATA_TYPE NAME) { VAR_NAME = NAME; }
#define DECLARE_GETTER_SETTER(DATA_TYPE, NAME, VAR_NAME)			DECLARE_GETTER(DATA_TYPE, NAME, VAR_NAME) DECLARE_SETTER(DATA_TYPE, NAME, VAR_NAME)
