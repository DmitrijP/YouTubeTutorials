package de.dmitrij.patuk.insurance.backend.api.controller;

import de.dmitrij.patuk.insurance.backend.api.dto.UserProfileCreationDto;
import de.dmitrij.patuk.insurance.backend.api.dto.UserProfileCreationResultDto;
import de.dmitrij.patuk.insurance.backend.api.mapper.UserProfileEntityToDtoMapper;
import de.dmitrij.patuk.insurance.backend.data.entities.UserProfileEntity;
import de.dmitrij.patuk.insurance.backend.data.repositories.UserProfileRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/v1/user-profile")
public class UserProfileController {

    private final UserProfileRepository userProfileRepository;
    private final UserProfileEntityToDtoMapper mapper;

    @Autowired
    public UserProfileController(UserProfileRepository userProfileRepository,
                                 UserProfileEntityToDtoMapper mapper) {
        this.userProfileRepository = userProfileRepository;
        this.mapper = mapper;
    }

    @PostMapping("create")
    public ResponseEntity<UserProfileEntity> addUserProfile(
            @RequestParam("firstName") String firstName,
            @RequestParam("lastName") String lastName,
            @RequestParam("userName") String userName){
        var userProfile = mapper.map(firstName,lastName, userName);
        var savedProfile = userProfileRepository.save(userProfile);
        return ResponseEntity.ok(savedProfile);
    }

    @PostMapping("create-with-json")
    public ResponseEntity<UserProfileCreationResultDto> addUserProfileJson(@RequestBody UserProfileCreationDto profile){
        var userProfile = mapper.map(profile);
        var savedProfile = userProfileRepository.save(userProfile);
        var dto = mapper.map(savedProfile);
        return ResponseEntity.ok(dto);
    }

    @GetMapping("find-by-id")
    public ResponseEntity<UserProfileCreationResultDto> addUserProfile(@RequestParam("id") String id) {
        var foundProfileOpt = userProfileRepository.findById(id);
        if(foundProfileOpt.isPresent()){
            var savedProfile = foundProfileOpt.get();
            var dto = mapper.map(savedProfile);
            return ResponseEntity.ok(dto);
        }
        return new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @GetMapping("find-all")
    public ResponseEntity<List<UserProfileCreationResultDto>> findProfiles()  {
        var foundProfileOpt = userProfileRepository.findAll();
        var mappedDtos = foundProfileOpt.stream()
                .map(mapper::map)
                .collect(Collectors.toList());
        return ResponseEntity.ok(mappedDtos);
    }



    @DeleteMapping("delete-by-id")
    public ResponseEntity deleteById(String id) {
        var profileOpt = userProfileRepository.findById(id);
        if(profileOpt.isPresent()){
            userProfileRepository.delete(profileOpt.get());
            return new ResponseEntity<>(HttpStatus.OK);
        }
        return new ResponseEntity(HttpStatus.NO_CONTENT);
    }
}
