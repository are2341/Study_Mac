package Practice_030;

import java.util.Scanner;

//! Practice 30
public class Practice_030 {
	//! 초기화
	public static void start(String[] args) {
//		P07.start(args);
//		P08.start(args);
		P09.start(args);
	}
	
	//! P07
	private static class P07 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			P07.printSumValue(nValue);
		}
		
		//! 합계를 출력한다
		private static void printSumValue(int a_nValue) {
			int nSumValue = 0;
			
			for(int i = 0; i < a_nValue; ++i) {
				nSumValue += i + 1;
				System.out.printf("%d %c ", i + 1, (i < a_nValue - 1) ? '+' : '=');
			}
			
			System.out.printf("%d\n", nSumValue);
		}
	}
	
	//! P08
	private static class P08 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			
			int nSumValue = P08.getSumValue(nValue);
			System.out.printf("합계 : %d\n", nSumValue);
		}
		
		//! 합계를 출력한다
		private static int getSumValue(int a_nValue) {
			return (a_nValue * (1 + a_nValue)) / 2;
		}
	}
	
	//! P09
	private static class P09 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 (2 개) 입력 : ");
			
			int nValueA = oScanner.nextInt();
			int nValueB = oScanner.nextInt();
			
			int nSumValue = P09.getSumValue(nValueA, nValueB);
			System.out.printf("합계 : %d\n", nSumValue);
		}
		
		//! 합계를 출력한다
		private static int getSumValue(int a_nValueA, int a_nValueB) {
			int nSumValue = 0;
			
			// A 값이 클 경우
			if(a_nValueA > a_nValueB) {
				int nTemp = a_nValueA;
				a_nValueA = a_nValueB;
				a_nValueB = nTemp;
			}
			
			for(int i = a_nValueA; i <= a_nValueB; ++i) {
				nSumValue += i;
			}
			
			return nSumValue;
		}
	}
}
