//
//  main.cpp
//  E01
//
//  Created by 이상동 on 2021/04/12.
//

#include "Example_058/Example_058.hpp"
#include "Example_063/Example_063.hpp"
#include "Example_067/Example_067.hpp"
#include "Example_070/Example_070.hpp"
#include "Example_077/Example_077.hpp"

/*
 C 알고리즘 예제 프로젝트 리스트
 :
 - E01 (처음 만나는 알고리즘)
 - E02 (뇌를 자극하는 알고리즘)
 - E03 (C 언어로 배우는 알고리즘 입문)
 */
//! 메인 함수
int main(const int argc, const char **args) {
	srand((unsigned int)time(NULL));
	
//	E058::Example_058(argc, args);
//	E063::Example_063(argc, args);
//	E067::Example_067(argc, args);
//	E070::Example_070(argc, args);
	E077::Example_077(argc, args);
	
	return 0;
}
