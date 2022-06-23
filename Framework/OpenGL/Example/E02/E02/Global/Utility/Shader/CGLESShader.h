//
//  CGLESShader.h
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "../../Define/KCDefine.h"

/** GLES 쉐이더 */
class CGLESShader {
public:
	
	/** 매개 변수 */
	struct STParams {
		std::string m_oVSFilePath;
		std::string m_oFSFilePath;
	};
	
public:			// getter
	
	/** 프로그램 */
	DECLARE_GETTER(GLuint, Program, m_nProgram);
	
	/** 정점 쉐이더 */
	DECLARE_GETTER(GLuint, VertexShader, m_nVertexShader);
	
	/** 파편 쉐이더 */
	DECLARE_GETTER(GLuint, FragmentShader, m_nFragmentShader);
	
public:			// public 함수
	
	/** 초기화 */
	virtual void Init(STParams a_stParams);
	
private:			// private 함수
	
	/** 프로그램을 설정한다 */
	void SetupProgram(void);
	
	/** 정점 쉐이더를 설정한다 */
	void SetupVSShader(void);
	
	/** 파편 쉐이더를 설정한다 */
	void SetupFSShader(void);
	
private:			// private 변수
	
	GLuint m_nProgram = 0;
	GLuint m_nVertexShader = 0;
	GLuint m_nFragmentShader = 0;
	
	STParams m_stParams;
};
