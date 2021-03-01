package Practice_033;

import java.util.Scanner;

//! Practice 33
public class Practice_033 {
	//! 초기화
	public static void start(String[] args) {
//		P10.start(args);
		P11.start(args);
	}
	
	//! P10
	private static class P10 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			
			int nValueA = 0;
			int nValueB = 0;
			
			do {
				System.out.print("정수 (2 개) 입력 : ");
				
				nValueA = oScanner.nextInt();
				nValueB = oScanner.nextInt();
				
				if(nValueB <= nValueA) {
					System.out.print("우항의 값은 좌항의 값보다 커야합니다.\n\n");
				}
			} while(nValueA >= nValueB);
			
			int nSubValue = P10.getSubValue(nValueA, nValueB);
			System.out.printf("결과 : %d\n", nSubValue);
		}
		
		//! 뺄셈 값을 반환한다
		private static int getSubValue(int a_nLhs, int a_nRhs) {
			return a_nRhs - a_nLhs;
		}
	}
	
	//! P11
	private static class P11 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			P11.printNumDigits(nValue);
		}
		
		//! 자릿수를 출력한다
		private static void printNumDigits(int a_nValue) {
			int nNumDigits = 1;
			
			while(a_nValue / 10 != 0) {
				a_nValue /= 10;
				nNumDigits += 1;
			}
			
			System.out.printf("%d 자리 수입니다.\n", nNumDigits);
		}
	}
}
