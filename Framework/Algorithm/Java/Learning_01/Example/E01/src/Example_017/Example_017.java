package Example_017;

//! Example 17
public class Example_017 {
	//! 초기화
	public static void start(String[] args) {
		for(int i = 1; i <= 3; ++i) {
			for(int j = 1; j <= 3; ++j) {
				for(int k = 1; k <= 3; ++k) {
					System.out.printf("최대 값 (%d, %d, %d) : %d\n", i, j, k, Example_017.getMaxValue(i, j, k));
				}
			}
		}
	}
	
	//! 최대 값을 반환한다
	private static int getMaxValue(int a_nValueA, int a_nValueB, int a_nValueC) {
		int nMaxValue = (a_nValueA >= a_nValueB) ? a_nValueA : a_nValueB;
		return (nMaxValue >= a_nValueC) ? nMaxValue : a_nValueC;
	}
}
