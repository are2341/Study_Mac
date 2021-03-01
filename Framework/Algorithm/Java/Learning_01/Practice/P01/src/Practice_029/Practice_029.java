package Practice_029;

import java.util.Scanner;

//! Practice 29
public class Practice_029 {
	//! 초기화
	public static void start(String[] args) {
		P06.start(args);
	}
	
	//! P06
	private static class P06 {
		//! 초기화
		public static void start(String[] args) {
			Scanner oScanner = new Scanner(System.in);
			System.out.print("정수 입력 : ");
			
			int nValue = oScanner.nextInt();
			
			int i = 0;
			int nSumValue = 0;
			
			while(i < nValue) {
				nSumValue += i + 1;
				i += 1;
			}
			
			System.out.printf("합계 (%d) : %d\n", nValue, nSumValue);
		}
	}
}
