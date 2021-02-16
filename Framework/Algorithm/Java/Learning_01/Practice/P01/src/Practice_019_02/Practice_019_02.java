package Practice_019_02;

import java.util.Scanner;

//! Example 19 - 2
public class Practice_019_02 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (3 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		
		int nMinValue = Practice_019_02.getMinValue(nValueA, nValueB, nValueC);
		System.out.printf("최소 값 : %d\n", nMinValue);
	}
	
	//! 최소 값을 반환한다
	private static int getMinValue(int a_nValueA, int a_nValueB, int a_nValueC) {
		int nMinValue = (a_nValueA <= a_nValueB) ? a_nValueA : a_nValueB;
		return (nMinValue <= a_nValueC) ? nMinValue : a_nValueC;
	}
}
