# Quiz/Poll Application
Create a simple quiz/poll application.<br>
Create quiz (admin only), answer to quiz (public or registered users). Quiz questions should be of multiple-choice type (ie many choices, only one
can be selected - radio buttons). One answer can be marked as correct choice during quiz creation for statistics (but not mandatory, some
quizzes do not have any correct answers - polls for example).<br>
* Quiz - only one answer can be marked as correct.<br>
* Poll - no answer is correct: we are just collecting users feedback.
Front page shows statistics/results of all the quizzes and possibility to go answer to the quizzes.<br>
Simple MVC for backend admin (simple crud in backend only), client app technology is Aurelia, as specified in JavaScript (ICD0006) exam.
The use of circular reference serialization is not allowed (ie. using DTOs is necessary).<br>
Backend patterns are free choice: UnitOfWork, Repository Pattern, API versioning and Mapping are implemented in our project. Role implementation is mandatory.<br>
ASP.NET - Full app, no ViewBag/ViewData is allowed! Partial views must be implemented where possible.
