//
//  Example_150.cpp
//  E01
//
//  Created by 이상동 on 2022/06/03.
//

#include "Example_150.hpp"

namespace E150 {
	/** 객체를 렌더링한다 */
	void Render(void) {
		glClear(GL_COLOR_BUFFER_BIT);
		glBegin(GL_POLYGON);
		
		glVertex3f(-0.5f, -0.5f, 0.0f);
		glVertex3f(-0.5f, 0.5f, 0.0f);
		glVertex3f(0.5f, 0.5f, 0.0f);
		glVertex3f(0.5f, -0.5f, 0.0f);
		
		glEnd();
		glFlush();
	}
	
	/** Example 150 */
	int Example_150(const int argc, const char **args) {
		glutCreateWindow("Example_150");
		glutDisplayFunc(Render);
		
		glutMainLoop();
		return 0;
	}
}
