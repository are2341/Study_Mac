//
//  CGLESView.m
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "CGLESView.h"

/** GLES 뷰 */
@implementation CGLESView
// 프로퍼티
@synthesize colorBuffer = m_nColorBuffer;
@synthesize depthBuffer = m_nDepthBuffer;
@synthesize stencilBuffer = m_nStencilBuffer;
@synthesize frameBuffer = m_nFrameBuffer;

#pragma mark - 클래스 메서드
/** 레이어 클래스를 생성한다 */
+ (Class)layerClass {
	return [CAEAGLLayer class];
}

#pragma mark - getter
/** GLES 레이어를 반환한다 */
- (CAEAGLLayer *)GLESLayer {
	// GLES 레이어가 없을 경우
	if(m_pGLESLayer == nil) {
		m_pGLESLayer = [[CAEAGLLayer alloc] initWithLayer:self.layer];
	}
	
	return m_pGLESLayer;
}

#pragma mark - 인스턴스 메서드
/** 초기화 */
- (id)initWithFrame:(CGRect)a_stFrame {
	// 초기화 되었을 경우
	if(self = [super initWithFrame:a_stFrame]) {
		[self setupColorBuffer];
		[self setupDepthBuffer];
		[self setupStencilBuffer];
		[self setupFrameBuffer];
	}
	
	return self;
}

/** 색상 버퍼를 설정한다 */
- (void)setupColorBuffer {
	glGenRenderbuffers(G_VAL_1_INT, &m_nColorBuffer);
	glBindRenderbuffer(GL_RENDERBUFFER, m_nColorBuffer);
}

/** 깊이 버퍼를 설정한다 */
- (void)setupDepthBuffer {
	glGenRenderbuffers(G_VAL_1_INT, &m_nDepthBuffer);
	glBindRenderbuffer(GL_RENDERBUFFER, m_nDepthBuffer);
}

/** 스텐실 버퍼를 설정한다 */
- (void)setupStencilBuffer {
	glGenRenderbuffers(G_VAL_1_INT, &m_nStencilBuffer);
	glBindRenderbuffer(GL_RENDERBUFFER, m_nStencilBuffer);
}

/** 프레임 버퍼를 설정한다 */
- (void)setupFrameBuffer {
	glGenFramebuffers(G_VAL_1_INT, &m_nFrameBuffer);
	glBindFramebuffer(GL_FRAMEBUFFER, m_nFrameBuffer);
	
	glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_COLOR_ATTACHMENT0, GL_RENDERBUFFER, self.colorBuffer);
	glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_DEPTH_ATTACHMENT, GL_RENDERBUFFER, self.depthBuffer);
	glFramebufferRenderbuffer(GL_FRAMEBUFFER, GL_STENCIL_ATTACHMENT, GL_RENDERBUFFER, self.stencilBuffer);
}
@end			// CGLESView
