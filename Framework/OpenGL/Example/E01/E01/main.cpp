//
//  main.cpp
//  E01
//
//  Created by 이상동 on 2022/06/03.
//

#include "Example_150/Example_150.hpp"

/** 메인 함수 */
int main(const int argc, const char **args) {
	glutInit((int *)&argc, (char **)args);
	glutInitWindowSize(G_WND_WIDTH, G_WND_HEIGHT);
	
	return E150::Example_150(argc, args);
}
