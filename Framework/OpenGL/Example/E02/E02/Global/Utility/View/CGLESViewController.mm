//
//  CGLESViewController.m
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "CGLESViewController.h"
#import "CGLESView.h"

/** GLES 뷰 컨트롤러 */
@implementation CGLESViewController
#pragma mark - getter
/** GLES 뷰를 반환한다 */
- (CGLESView *)GLESView {
	// GLES 뷰가 없을 경우
	if(m_pGLESView == nil) {
		m_pGLESView = [[CGLESView alloc] initWithFrame:self.view.bounds];
		[self.view addSubview:m_pGLESView];
	}
	
	return m_pGLESView;
}

#pragma mark - 인스턴스 메서드
/** 뷰가 생성 되었을 경우 */
- (void)viewDidLoad {
	[super viewDidLoad];
	
	[self setupContext];
	[self setupRenderingLoop];
}

/** 컨텍스트를 설정한다 */
- (void)setupContext {
	self.GLESContext = [[EAGLContext alloc] initWithAPI:kEAGLRenderingAPIOpenGLES2];
	[self.GLESContext renderbufferStorage:GL_RENDERBUFFER fromDrawable:self.GLESView.GLESLayer];
	
	[EAGLContext setCurrentContext:self.GLESContext];
}

/** 렌더링 루프를 설정한다 */
- (void)setupRenderingLoop {
	self.displayLink = [CADisplayLink displayLinkWithTarget:self selector:@selector(render:)];
	[self.displayLink addToRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
}

/** 물체를 그린다 */
- (void)render:(CADisplayLink *)a_pDisplayLink {
	[self preRender:a_pDisplayLink];
	[self doRender:a_pDisplayLink];
	[self postRender:a_pDisplayLink];
	
	[self.GLESContext presentRenderbuffer:GL_RENDERBUFFER];
}

/** 물체를 그릴 경우 */
- (void)preRender:(CADisplayLink *)a_pDisplayLink {
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
	glClearDepthf(0.0f);
	glClearStencil(0);
	
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
}

/** 물체를 그린다 */
- (void)doRender:(CADisplayLink *)a_pDisplayLink {
	// Do Something
}

/** 물체를 그렸을 경우 */
- (void)postRender:(CADisplayLink *)a_pDisplayLink {
	// Do Something
}
@end			// CGLESViewController
