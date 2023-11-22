package de.dmitrij.patuk.insurance.backend.api.controller;

import de.dmitrij.patuk.insurance.backend.data.entities.UserProfileEntity;
import de.dmitrij.patuk.insurance.backend.data.repositories.UserProfileRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/v1/user-profile")
public class UserProfileController {

    private final UserProfileRepository userProfileRepository;
    @Autowired
    public UserProfileController(UserProfileRepository userProfileRepository) {
        this.userProfileRepository = userProfileRepository;
    }

    @GetMapping("greeting")
    public String greet(@RequestParam("name") String name){
        return "Hello " + name;
    }

    @PostMapping("create")
    public String addUserProfile(@RequestParam("firstName") String firstName,
                                 @RequestParam("lastName") String lastName,
                                 @RequestParam("userName") String userName){
        var userProfile = new UserProfileEntity();
        userProfile.setFirstName(firstName);
        userProfile.setLastName(lastName);
        userProfile.setUserName(userName);
        var savedProfile = userProfileRepository.save(userProfile);
        return savedProfile.getId();
    }

    @GetMapping("find-by-id")
    public UserProfileEntity addUserProfile(@RequestParam("id") String id) throws Exception {
        var foundProfileOpt = userProfileRepository.findById(id);
        if(foundProfileOpt.isPresent()){
            return foundProfileOpt.get();
        }
        throw new Exception("whatever");
    }

    @GetMapping("find-all")
    public List<UserProfileEntity> findProfiles() throws Exception {
        var foundProfileOpt = userProfileRepository.findAll();
        return foundProfileOpt;
    }

    @DeleteMapping("delete-by-id")
    public String deleteById(String id) throws Exception {
        var profileOpt = userProfileRepository.findById(id);
        if(profileOpt.isPresent()){
            userProfileRepository.delete(profileOpt.get());
            return "deleted";
        }
        return "no entity existed with id " + id;
    }
}
