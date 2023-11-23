package de.dmitrij.patuk.insurance.backend.api.controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
@RequestMapping("/v1/administration")
public class AdministrationController {

    @GetMapping("/index")
    public String index(Model model){
        model.addAttribute("address", "Administration Controller -  Index Method");
        model.addAttribute("name", "Dmitrij Patuk");
        return "/administration/index";
    }
}
