//
//  CGLESViewController.h
//  E02
//
//  Created by 이상동 on 2022/06/23.
//

#import "../../Define/KGDefine.h"

NS_ASSUME_NONNULL_BEGIN

@class CGLESView;

/** GLES 뷰 컨트롤러 */
@interface CGLESViewController : UIViewController {
	CGLESView *m_pGLESView;
}

// 프로퍼티 {
@property(nonatomic, strong) EAGLContext *GLESContext;
@property(nonatomic, strong) CADisplayLink *displayLink;

@property(nonatomic, strong, readonly) CGLESView *GLESView;
// 프로퍼티 }

/** 물체를 그릴 경우 */
- (void)preRender:(CADisplayLink *)a_pDisplayLink;

/** 물체를 그린다 */
- (void)doRender:(CADisplayLink *)a_pDisplayLink;

/** 물체를 그렸을 경우 */
- (void)postRender:(CADisplayLink *)a_pDisplayLink;

@end			// CGLESViewController

NS_ASSUME_NONNULL_END
