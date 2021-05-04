//
//  main.cpp
//  E01
//
//  Created by 이상동 on 2021/04/18.
//

#include "Example_019/Example_019.hpp"
#include "Example_028/Example_028.hpp"
#include "Example_039/Example_039.hpp"
#include "Example_052/Example_052.hpp"
#include "Example_055/Example_055.hpp"

/*
 C 자료구조 프로젝트 리스트
 :
 - E01 (열혈 자료구조)
 */
//! 메인 함수
int main(const int argc, const char **args) {
	srand((unsigned int)time(NULL));
	
//	E019::Example_019(argc, args);
//	E028::Example_028(argc, args);
//	E039::Example_039(argc, args);
//	E052::Example_052(argc, args);
	E055::Example_055(argc, args);
	
	return 0;
}
