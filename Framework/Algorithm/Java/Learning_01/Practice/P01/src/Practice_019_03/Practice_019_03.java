package Practice_019_03;

import java.util.Scanner;

//! Practice 19 - 3
public class Practice_019_03 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (4 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		int nValueD = oScanner.nextInt();
		
		int nMinValue = Practice_019_03.getMinValue(nValueA, nValueB, nValueC, nValueD);
		System.out.printf("최소 값 : %d\n", nMinValue);
	}
	
	//! 최소 값을 반환한다
	private static int getMinValue(int a_nValueA, int a_nValueB, int a_nValueC, int a_nValueD) {
		int nMinValueA = (a_nValueA <= a_nValueB) ? a_nValueA : a_nValueB;
		int nMinValueB = (a_nValueC <= a_nValueD) ? a_nValueC : a_nValueD;
		
		return (nMinValueA <= nMinValueB) ? nMinValueA : nMinValueB;
	}
}
