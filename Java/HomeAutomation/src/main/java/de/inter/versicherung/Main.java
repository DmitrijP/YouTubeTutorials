package de.inter.versicherung;

import de.inter.versicherung.haus.automatisierung.Building;

import java.util.Scanner;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in)) {
            InputOutputHandler ioHandler = new InputOutputHandler(scanner);
            BuildingManager b = new BuildingManager(ioHandler);

            ioHandler.println("We will create a building now!");
            b.createBuilding();
            fillBuilding(ioHandler, b);
        }
    }


    public static void fillBuilding(InputOutputHandler ioHandler, BuildingManager b){
        int level = ioHandler.getIntFromUserForReason("What floor do you wish to fill with rooms?");
        b.fillFloorWithRooms(level);
        ioHandler.println("Floor filled with rooms, not it is time to fill a room");
        level = ioHandler.getIntFromUserForReason("What floor is your room on?");
        String roomName = ioHandler.getStringFromUserForReason("What is the name of the room you wish to fill?");
        b.fillRoom(level, roomName);
        ioHandler.println(b.toString());

        String answer = ioHandler.getStringFromUserForReason("Do you wish to start over filling the building? Y=yes, ENTER to quit");
        if(!"Y".equals(answer)){
            return;
        }

        fillBuilding(ioHandler, b);
    }
}