package Practice_019;

import java.util.Scanner;

//! Practice 19
public class Practice_019 {
	//! 초기화
	public static void start(String[] args) {
//		P01.start(args);
//		P02.start(args);
		P03.start(args);
	}
	
	//! P01
	private static class P01 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 (4 개) 입력 : ");
			
			int nValueA = oScanner.nextInt();
			int nValueB = oScanner.nextInt();
			int nValueC = oScanner.nextInt();
			int nValueD = oScanner.nextInt();
			
			int nMaxValue = P01.getMaxValue(nValueA, nValueB, nValueC, nValueD);
			System.out.printf("최대 값 : %d\n", nMaxValue);
		}
		
		//! 최대 값을 반환한다
		private static int getMaxValue(int a_nValueA, int a_nValueB, int a_nValueC, int a_nValueD) {
			int nMaxValueA = (a_nValueA >= a_nValueB) ? a_nValueA : a_nValueB;
			int nMaxValueB = (a_nValueC >= a_nValueD) ? a_nValueC : a_nValueD;
			
			return (nMaxValueA >= nMaxValueB) ? nMaxValueA : nMaxValueB;
		}
	}
	
	//! P02
	private static class P02 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 (3 개) 입력 : ");
			
			int nValueA = oScanner.nextInt();
			int nValueB = oScanner.nextInt();
			int nValueC = oScanner.nextInt();
			
			int nMaxValue = P02.getMinValue(nValueA, nValueB, nValueC);
			System.out.printf("최소 값 : %d\n", nMaxValue);
		}
		
		//! 최소 값을 반환한다
		private static int getMinValue(int a_nValueA, int a_nValueB, int a_nValueC) {
			int nMinValue = (a_nValueA <= a_nValueB) ? a_nValueA : a_nValueB;
			return (nMinValue <= a_nValueC) ? nMinValue : a_nValueC;
		}
	}
	
	//! P03
	private static class P03 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 (4 개) 입력 : ");
			
			int nValueA = oScanner.nextInt();
			int nValueB = oScanner.nextInt();
			int nValueC = oScanner.nextInt();
			int nValueD = oScanner.nextInt();
			
			int nMaxValue = P03.getMinValue(nValueA, nValueB, nValueC, nValueD);
			System.out.printf("최소 값 : %d\n", nMaxValue);
		}
		
		//! 최소 값을 반환한다
		private static int getMinValue(int a_nValueA, int a_nValueB, int a_nValueC, int a_nValueD) {
			int nMinValueA = (a_nValueA <= a_nValueB) ? a_nValueA : a_nValueB;
			int nMinValueB = (a_nValueC <= a_nValueD) ? a_nValueC : a_nValueD;
			
			return (nMinValueA <= nMinValueB) ? nMinValueA : nMinValueB;
		}
	}
}
