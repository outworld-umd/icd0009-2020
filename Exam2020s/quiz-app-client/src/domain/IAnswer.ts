export interface IAnswer {
    id: string;
    choiceId: string;
    quizSessionId: string;
    isCorrect: boolean;
}

export interface IAnswerCreate {
    choiceId: string;
    isCorrect: boolean;
}
