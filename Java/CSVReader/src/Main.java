import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    public static void main(String[] args){
        String filePath = getUserInputForAReason("Hallo bitte gib den Pfad zu einer CSV Datei ein:");
        ArrayList<String> csv = getFile(filePath);
        printCsvProperties(csv);
        getUserInputAndExecuteMatchingCommand(csv);
    }

    private static void getUserInputAndExecuteMatchingCommand(ArrayList<String> csv){
        boolean continueAsLongAsNotQuit = true;
        while (continueAsLongAsNotQuit){
            String command = getUserInputForAReason("Folgende Commands sind möglich: quit, select-top n, select-bottom n....");
            if(command.contains("quit")){
                continueAsLongAsNotQuit = false;
            }
            if(command.contains("select-top")){
                executeSelectTopCommand(csv, command);
            }
            if(command.contains("select-bottom")){
                executeSelectBottomCommand(csv, command);
            }
        }
    }

    private static void executeSelectBottomCommand(ArrayList<String> csv, String command) {

    }

    private static void executeSelectTopCommand(ArrayList<String> csv, String command) {
        String number = command.split(" ")[1];
        int parsedNumber = Integer.parseInt(number);
        for (int i  = 0; i < parsedNumber; i++)
            print(csv.get(i));
    }

    private static void printCsvProperties(ArrayList<String> csv){
        int lines = csv.size();
        int columns = csv.get(0).split(";").length;
        print("File was read. It has " +  lines + "lines and " + columns + " columns" );
    }

    private static ArrayList<String> getFile(String filePath){
        try {
            BufferedReader bufferedReader = new BufferedReader(new FileReader(filePath));
            String line;
            ArrayList<String> file = new ArrayList<>();
            while ((line = bufferedReader.readLine()) != null){
                file.add(line);
            }
            return file;
        } catch (FileNotFoundException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private static String getUserInputForAReason(String reason){
        print(reason);
        return getUserInput();
    }

    private static String getUserInput(){
        Scanner scanner = new Scanner(System.in);
        String userInput = scanner.nextLine();
        return userInput;
    }

    private static void print(String message){
        System.out.println(message);
    }
}
