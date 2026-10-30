import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class TextAnalyzer {
    public static void main(String[] args) {
        int lineCount = 0;
        int wordCount = 0;
        String longestWord = "";
        int javaLineCount = 0;
        boolean isEmpty = true;

        try (BufferedReader reader = new BufferedReader(new FileReader("text.txt"))) {
            String line;
            while ((line = reader.readLine()) != null) {
                isEmpty = false;
                lineCount++;
                
                if (line.contains("Java")) {
                    javaLineCount++;
                }
                
                // Разделение строки на слова по пробелам
                String[] words = line.trim().split("\\s+");
                if (words.length > 0 && !words[0].isEmpty()) {
                    wordCount += words.length;
                    for (String word : words) {
                        if (word.length() > longestWord.length()) {
                            longestWord = word;
                        }
                    }
                }
            }

            if (isEmpty) {
                System.out.println("Файл пуст");
            } else {
                System.out.println("Строк: " + lineCount);
                System.out.println("Слов: " + wordCount);
                System.out.println("Самое длинное слово: " + longestWord);
                System.out.println("Строк с \"Java\": " + javaLineCount);
            }

        } catch (IOException e) {
            System.out.println("Ошибка чтения файла: " + e.getMessage());
        }
    }
}
