package Example_017;

//! Example 17
public class Example_017 {
	//! 초기화
	public static void start(String[] args) {
		for(int i = 1; i <= 3; ++i) {
			for(int j = 1; j <= 3; ++j) {
				for(int k = 1; k <= 3; ++k) {
					// 값이 모두 다를 경
					if(i != j && i != k && j != k) {
						int nMaxValue = Example_017.getMaxValue(i, j, k);
						System.out.println(String.format("최대 값 (%d, %d, %d) : %d", i, j, k, nMaxValue)); 
					}
				}
			}
		}
	}
	
	//! 최대 값을 반환한다
	private static int getMaxValue(int a_nLhs, int a_nMhs, int a_nRhs) {
		int nMaxValue = (a_nLhs >= a_nMhs) ? a_nLhs : a_nMhs;
		return (nMaxValue >= a_nRhs) ? nMaxValue : a_nRhs;
	}
}
