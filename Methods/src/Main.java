import java.util.Scanner;
public class Main {
    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        System.out.println("==================================================");
        System.out.println("Меню программы");
        System.out.println("1.Проверка на четность 5 чисел");
        System.out.println("2.Проверка на палиндром");
        System.out.println("3.Нахождение факториала");
        System.out.println("4.Три метода(сумма цифр числа, количество, среднее арифметическое");
        System.out.println("==================================================");
        int num = in.nextInt();
        if (num == 1) {
            System.out.println("Введите 5 чисел");
            for (int i = 0; i < 5; i++) {
                int ty = in.nextInt();
                System.out.println(isEven(ty));
                ty = 0;
            }
        }
        else if (num == 2) {
            System.out.println("Введите строку");
            in.nextLine();
            String n = in.nextLine();
            System.out.println(isPalindrome(n));
        } else if (num == 3) {
            System.out.println("Введите число");
            try {
                int as = in.nextInt();
                System.out.println(factorial(as));
            }
            catch (Exception e) {
                System.out.println("введи число");
                return;
            }
        } else if (num == 4) {
            try {
                System.out.println("Введите строку");
                int n = in.nextInt();
                System.out.println("сумма цифр числа");
                System.out.println(SummaNumbers(n));
                System.out.println("количество цифр");
                System.out.println(QuantityNumbers(n));
                System.out.println("cреднее арифметическое этих цифр ");
                System.out.println(MidAriphmetic(n));
            } catch (Exception e) {
                System.out.println("введи число");
            }
        }
    }
    static Boolean isEven(int n) {

        if(n % 2 == 0) {
            return true;
        }
        else {
            return false;
        }
    }
    static Boolean isPalindrome(String s) {
        String cl = s.toLowerCase().replace(" ", "");
        String a = new StringBuilder(cl).reverse().toString();

        if (cl.equals(a)) {
            return true;
        }
        else {
            return false;
        }
    }
    static int factorial(int n) {
        if(n == 1) {
            return n;
        }
        if (n < 0) {
            return -1;
        }
        if (n == 0) {
            return 1;
        }
        return n * factorial(n - 1);
    }
    static double SummaNumbers(int n) {
        double sum = 0;
        if(n < 0) {
            Math.abs(n);
        }
        while(n > 0) {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }
    static double QuantityNumbers(int n) {
        double sum = 0;
        double t = 0;
        while (n > 0) {
            t += n % 10;
            n /= 10;
            sum++;
        }
        return sum;
    }
    static double MidAriphmetic(int n) {
        double sum = (SummaNumbers(n)) / QuantityNumbers(n);
        return sum;
    }
    static int GWords(String s) {
        String words = "euioaуеаояюэи";
        int sum = 0;
        for (int i = 0; i < s.length(); i++) {
            char ai = s.charAt(i);
            if (words.indexOf(ai) != -1) {
                sum++;
            }
        }
        return sum;
    }
    static int SWords(String s) {
        String words = "qwrtpsdfghjklzxcvbnmйцкнгшщзфвпрлджчсмтьъб";
        int sum = 0;
        for (int i = 0; i < s.length(); i++) {
            char zapyataya = s.charAt(i);
            if (words.indexOf(zapyataya) != -1) {
                sum++;
            }
        }
        return sum;
    }

}