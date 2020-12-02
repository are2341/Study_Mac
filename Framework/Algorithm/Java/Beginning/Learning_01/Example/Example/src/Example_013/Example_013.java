package Example_013;

import java.util.Scanner;

public class Example_013 {
	//! 초기화
	public static void start(String[] args) {
		Scanner oScanner = new Scanner(System.in);
		System.out.print("정수 (3 개) 입력 : ");
		
		int nValueA = oScanner.nextInt();
		int nValueB = oScanner.nextInt();
		int nValueC = oScanner.nextInt();
		
		int nMax = (nValueA >= nValueB) ? nValueA : nValueB;
		nMax = (nMax >= nValueC) ? nMax : nValueC;
		
		System.out.println("최대 값 : " + nMax);
	}
}
