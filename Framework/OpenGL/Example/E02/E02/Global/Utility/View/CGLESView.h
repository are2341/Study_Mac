//
//  CGLESView.h
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "../../Define/KGDefine.h"

NS_ASSUME_NONNULL_BEGIN

/** GLES 뷰 */
@interface CGLESView : UIView {
	GLuint m_nColorBuffer;
	GLuint m_nDepthBuffer;
	GLuint m_nStencilBuffer;
	GLuint m_nFrameBuffer;
	
	CAEAGLLayer *m_pGLESLayer;
}

// 프로퍼티 {
@property(nonatomic) GLuint colorBuffer;
@property(nonatomic) GLuint depthBuffer;
@property(nonatomic) GLuint stencilBuffer;
@property(nonatomic) GLuint frameBuffer;

@property(nonatomic, strong, readonly) CAEAGLLayer *GLESLayer;
// 프로퍼티 }
@end			// CGLESView

NS_ASSUME_NONNULL_END
