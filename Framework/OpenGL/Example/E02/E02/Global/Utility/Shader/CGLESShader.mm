//
//  CGLESShader.m
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "CGLESShader.h"

/** 초기화 */
void CGLESShader::Init(STParams a_stParams) {
	m_stParams = a_stParams;
	
	this->SetupVSShader();
	this->SetupFSShader();
	this->SetupProgram();
}

/** 프로그램을 설정한다 */
void CGLESShader::SetupProgram(void) {
	m_nProgram = glCreateProgram();
	glAttachShader(m_nProgram, m_nVertexShader);
	glAttachShader(m_nProgram, m_nFragmentShader);
}

/** 정점 쉐이더를 설정한다 */
void CGLESShader::SetupVSShader(void) {
	m_nVertexShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(m_nVertexShader, G_VAL_1_INT, nullptr, nullptr);
}

/** 파편 쉐이더를 설정한다 */
void CGLESShader::SetupFSShader(void) {
	m_nFragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
}
