package de.inter.versicherung;

import java.util.Scanner;

public class InputOutputHandler {
    private final Scanner consoleScanner;

    public InputOutputHandler(Scanner consoleScanner) {
        this.consoleScanner = consoleScanner;
    }

    public String getStringFromUserForReason(String reason) {
        println(reason);
        return getStringFromUser();
    }

    public int getIntFromUserForReason(String reason) {
        println(reason);
        return getIntFromUser();
    }

    public void println(String ln) {
        System.out.println(ln);
    }

    public String getStringFromUser() {
        return consoleScanner.nextLine();
    }

    public int getIntFromUser() {
        int i = consoleScanner.nextInt();
        //Clearing CR LF with nextLine()
        consoleScanner.nextLine();
        return i;
    }
}
