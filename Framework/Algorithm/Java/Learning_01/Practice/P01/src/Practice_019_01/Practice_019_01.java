package Practice_019_01;

import java.util.Scanner;

//! Practice 19 - 1
public class Practice_019_01 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (4 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		int nValueD = oScanner.nextInt();
		
		int nMaxValue = Practice_019_01.getMaxValue(nValueA, nValueB, nValueC, nValueD);
		System.out.printf("최대 값 : %d\n", nMaxValue); 
	}
	
	//! 최대 값을 반환한다
	private static int getMaxValue(int a_nValueA, int a_nValueB, int a_nValueC, int a_nValueD) {
		int nMaxValueA = (a_nValueA >= a_nValueB) ? a_nValueA : a_nValueB;
		int nMaxValueB = (a_nValueC >= a_nValueD) ? a_nValueC : a_nValueD;
		
		return (nMaxValueA >= nMaxValueB) ? nMaxValueA : nMaxValueB;
	}
}
