import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

public class FilterCopy {
    public static void main(String[] args) {
        int writtenLinesCount = 0;

        try (BufferedReader reader = new BufferedReader(new FileReader("input.txt"));
             BufferedWriter writer = new BufferedWriter(new FileWriter("output.txt"))) {

            String line;
            int outputLineNum = 1;

            while ((line = reader.readLine()) != null) {
                // Пропуск пустых строк
                if (line.trim().isEmpty()) {
                    continue;
                }
                
                // Условие: длина строки больше 10 символов
                if (line.length() > 10) {
                    writer.write(outputLineNum + ": " + line);
                    writer.newLine();
                    outputLineNum++;
                    writtenLinesCount++;
                }
            }
            writer.write("--- Конец ---");
            writer.newLine();

            System.out.println("Отфильтровано " + writtenLinesCount + " строк");

        } catch (IOException e) {
            System.out.println("Ошибка ввода-вывода: " + e.getMessage());
        }
    }
}
