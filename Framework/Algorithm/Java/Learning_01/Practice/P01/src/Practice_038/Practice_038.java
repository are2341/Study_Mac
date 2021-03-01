package Practice_038;

import java.util.Scanner;

//! Practice 38
public class Practice_038 {
	//! 초기화
	public static void start(String[] args) {
//		P12.start(args);
//		P13.start(args);
		P14.start(args);
	}
	
	//! P12
	private static class P12 {
		//! 초기화
		public static void start(String[] args) {
			P12.printMultiplyResults();
		}
		
		//! 곱셈 결과를 출력한다
		private static void printMultiplyResults() {
			System.out.print("  | ");
			
			for(int i = 1; i < 10; ++i) {
				System.out.printf("%-2d ", i);
			}
			
			System.out.println("\n--+---------------------------");
			
			for(int i = 1; i < 10; ++i) {
				System.out.printf("%d | ", i);
				
				for(int j = 1; j < 10; ++j) {
					System.out.printf("%-2d ", i * j);
				}
				
				System.out.println("");
			}
		}
	}
	
	//! P13
	private static class P13 {
		//! 초기화
		public static void start(String[] args) {
			P13.printSumResults();
		}
		
		//! 덧셈 결과를 출력한다
		private static void printSumResults() {
			System.out.print("  | ");
			
			for(int i = 1; i < 10; ++i) {
				System.out.printf("%-2d ", i);
			}
			
			System.out.println("\n--+---------------------------");
			
			for(int i = 1; i < 10; ++i) {
				System.out.printf("%d | ", i);
				
				for(int j = 1; j < 10; ++j) {
					System.out.printf("%-2d ", i + j);
				}
				
				System.out.println("");
			}
		}
	}
	
	//! P14
	private static class P14 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			P14.printRectStars(nValue);
		}
		
		//! 사각형 별을 출력한다
		private static void printRectStars(int a_nValue) {
			for(int i = 0; i < a_nValue; ++i) {
				for(int j = 0; j < a_nValue; ++j) {
					System.out.print("*");
				}
				
				System.out.println("");
			}
		}
	}
}
