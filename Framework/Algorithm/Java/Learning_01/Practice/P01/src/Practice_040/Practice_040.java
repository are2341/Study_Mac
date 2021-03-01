package Practice_040;

import java.util.Scanner;

//! Practice 40
public class Practice_040 {
	//! 초기화
	public static void start(String[] args) {
//		P15.start(args);
//		P16.start(args);
		P17.start(args);
	}
	
	//! P15
	private static class P15 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			
			System.out.println("===== 좌상단 별 출력 =====");
			P15.printTriangleLTStars(nValue);
			
			System.out.println("\n===== 좌하단 별 출력 =====");
			P15.printTriangleLBStars(nValue);
			
			System.out.println("\n===== 우상단 별 출력 =====");
			P15.printTriangleRTStars(nValue);
			
			System.out.println("\n===== 우하단 별 출력 =====");
			P15.printTriangleRBStars(nValue);
		}
		
		//! 좌상단 삼각형 별을 출력한다
		private static void printTriangleLTStars(int a_nValue) {
			for(int i = a_nValue - 1; i >= 0; --i) {
				for(int j = 0; j < a_nValue; ++j) {
					System.out.printf("%c", (j <= i) ? '*' : ' ');
				}
				
				System.out.println("");
			}
		}
		
		//! 좌하단 삼각형 별을 출력한다
		private static void printTriangleLBStars(int a_nValue) {
			for(int i = 0; i < a_nValue; ++i) {
				for(int j = 0; j < a_nValue; ++j) {
					System.out.printf("%c", (j <= i) ? '*' : ' ');
				}
				
				System.out.println("");
			}
		}
		
		//! 우상단 삼각형 별을 출력한다
		private static void printTriangleRTStars(int a_nValue) {
			for(int i = a_nValue - 1; i >= 0; --i) {
				for(int j = a_nValue - 1; j >= 0; --j) {
					System.out.printf("%c", (j <= i) ? '*' : ' ');
				}
				
				System.out.println("");
			}
		}
		
		//! 우하단 삼각형 별을 출력한다
		private static void printTriangleRBStars(int a_nValue) {
			for(int i = 0; i < a_nValue; ++i) {
				for(int j = a_nValue - 1; j >= 0; --j) {
					System.out.printf("%c", (j <= i) ? '*' : ' ');
				}
				
				System.out.println("");
			}
		}
	}
	
	//! P16
	private static class P16 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			P16.printPyramidStars(nValue);
		}
		
		//! 피라미드 별을 출력한다
		private static void printPyramidStars(int a_nValue) {
			int nMaxNumStars = ((a_nValue - 1)) * 2 + 1;
			
			for(int i = 0; i < a_nValue; ++i) {
				int nCenter = nMaxNumStars / 2;
				
				for(int j = 0; j < nMaxNumStars; ++j) {
					System.out.printf("%c", (j >= nCenter - i && j <= nCenter + i) ? '*' : ' ');
				}
				
				System.out.println("");
			}
		}
	}
	
	//! P17
	private static class P17 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			P17.printPyramidNumbers(nValue);
		}
		
		//! 피라미드 숫자를 출력한다
		private static void printPyramidNumbers(int a_nValue) {
			int nMaxNumStars = (a_nValue * 2) - 1;
			
			for(int i = 0; i < a_nValue; ++i) {
				int nNumber = i % 9;
				int nCenter = nMaxNumStars / 2;
				
				for(int j = 0; j < nMaxNumStars; ++j) {
					System.out.printf("%c", (j >= nCenter - i && j <= nCenter + i) ? '1' + nNumber : ' ');
				}
				
				System.out.println("");
			}
		}
	}
}
