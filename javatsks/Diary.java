import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

public class Diary {
    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in);
             BufferedWriter writer = new BufferedWriter(new FileWriter("diary.txt", true))) {
            
            int count = 0;
            while (true) {
                System.out.print("Введите заметку (или 'выход'): ");
                String input = scanner.nextLine();
                
                if ("выход".equalsIgnoreCase(input.trim())) {
                    break;
                }
                
                writer.write(input);
                writer.newLine();
                count++;
            }
            System.out.println("Сохранено " + count + " заметок");
            
        } catch (IOException e) {
            System.out.println("Ошибка при работе с файлом: " + e.getMessage());
        }
    }
}
