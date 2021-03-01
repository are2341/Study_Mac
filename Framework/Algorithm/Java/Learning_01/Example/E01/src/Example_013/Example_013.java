package Example_013;

import java.util.Scanner;

//! Example 13
public class Example_013 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (3 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		
		int nMaxValue = (nValueA >= nValueB) ? nValueA : nValueB;
		System.out.printf("최대 값 : %d\n", (nMaxValue >= nValueC) ? nMaxValue : nValueC);
	}
}
