import java.util.Scanner;
import java.util.Random;

void main() {
    Scanner in = new Scanner(System.in);
    String[] list = {"Москва", "Гуашь", "Красота"};
    String small = list[0].toLowerCase();
//    String[] mistakeList = { };
    LinkedHashSet<String> mistakeList = new LinkedHashSet<>();
    int mistake = 0;
//    String user = in.nextLine();

    while (mistake < 6) {
        System.out.print("Введите букву: ");
        String user = in.nextLine();
        char[] smList = small.toCharArray();
        for (char c : smList) {
            if (user.charAt(0) == c) {
                System.out.print(user);
            } else {
                mistake += 1;
                mistakeList.add(user);
                System.out.print("_");
            }
        }
        System.out.println();
        System.out.print("Ошибок: " + mistake + " из 6");
        System.out.println();
        System.out.print("Буквы которые не угадали: " + mistakeList); // придумать эту хуйню, через массив писать неправильнеы
        System.out.println();
    }

}
