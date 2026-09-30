import java.util.Scanner;
void main() {
    Scanner in = new Scanner(System.in);
//    int[] erd = {1, 3, 5, 3, 7, 1, 9, 5};
//    Set gl = new HashSet<>();
//
//    for(int i = 0; i < erd.length; i++) {
//        gl.add(erd[i]);
//    }
//    System.out.println(gl);

//    Set rt = new HashSet<>();
//    rt.add("Москва");
//    rt.add("Питер");
//    rt.add("Казань");
//    rt.add("Сочи");
//
//    String a = in.nextLine();
//    System.out.println(rt.contains(a));

//    Set tp = new HashSet<>();
//    String a = in.nextLine();
//
//
//    for(char k : a.toCharArray()) {
//        tp.add(k);
//    }
//    System.out.println(tp.size());

//    Set we = new HashSet<>();
//    Set qw = new HashSet<>();
//
//    we.add("Аня");
//    we.add("Иван");
//    we.add("Оля");
//    we.add("Петя");
//
//    qw.add("Иван");
//    qw.add("Оля");
//    qw.add("Маша");
//    qw.add("Дима");
//
//    we.retainAll(qw);
//    System.out.println(we);

//    Set set1 = new HashSet<>();
//    Set set2 = new HashSet<>();
//
//    set1.add(1);
//    set1.add(2);
//    set1.add(4);
//    set1.add(5);
//
//    set2.add(3);
//    set2.add(4);
//    set2.add(5);
//    set2.add(7);
//
//    Set set3 = new HashSet<>(set1);
//    Set set4 = new HashSet<>(set1);
//    Set set5 = new HashSet<>();
//    Set set6 = new HashSet<>();
//    Set set7 = new HashSet<>();
//    Set set8 = new HashSet<>();
//
//    set3.addAll(set2);
//    set4.retainAll(set2);
//    set5.removeAll(set2);
//    set6.removeAll(set1);
//    set7.contains(3);
//    set8.contains(3);
//
//    System.out.println("Объединение \n" + set3);
//    System.out.println("общие элементы двух множеств \n" + set4);
//    System.out.println("элементы, которые есть в A, но нет в B\n" + set5);
//    System.out.println("элементы, которые есть в В, но нет в А \n" + set6);
//    System.out.println("в каком из множеств есть число 3 первое \n" + set7);
//    System.out.println("в каком из множеств есть число 3 второе \n" + set8);

//    String a = "java python java c++ python java go";
//    String[] arr = a.split(" ");
//
//    Set set1 = new HashSet<>();
//
//    for(int i = 0; i < arr.length; i++) {
//        set1.add(arr[i]);
//    }
//
//    System.out.println("Уникальные слова " + set1);
//    System.out.println("Сколько уникальных слов " + set1.size());
//    System.out.println("Колво всех слов " + arr.length);

//    int[] arr = {1, 2, 3, 2, 4, 5, 1, 6, 5};
//
//    Set set1 = new HashSet<>();
//    Set set2 = new HashSet<>();
//
//    for (int i : arr) {
//        if (!set1.add(arr[i])) {
//            set2.add(arr[i]);
//        }
//    }
//    System.out.println(set2);

    Set set1 = new HashSet<>();
    String a = " ";
    String b = " ";
    char[] arrA = a.toCharArray();
    char[] arrB = b.toCharArray();


    while (a != "стоп" || b != "стоп") {
        a = in.nextLine();

        b = in.nextLine();
        if (arrA[0] != arrB[arrB.length - 1]) {
            System.out.println("Введите слово, несоответствует концу слова");
            b = in.nextLine();
        }

    }
}

