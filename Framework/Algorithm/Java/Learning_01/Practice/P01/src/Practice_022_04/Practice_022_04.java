package Practice_022_04;

import java.util.Scanner;

//! Practice 22 - 4
public class Practice_022_04 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (3 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		
		int nMidValue = Practice_022_04.getMidValue(nValueA, nValueB, nValueC);
		System.out.printf("중앙 값 : %d\n", nMidValue);
	}
	
	//! 중앙 값을 반환한다
	private static int getMidValue(int a_nValueA, int a_nValueB, int a_nValueC) {
		int nMaxValueA = (a_nValueA >= a_nValueB) ? a_nValueA : a_nValueB;
		int nMaxValueB = (a_nValueB >= a_nValueC) ? a_nValueB : a_nValueC;
		
		return (nMaxValueA <= nMaxValueB) ? nMaxValueA : nMaxValueB;
	}
}
