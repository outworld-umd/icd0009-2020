export interface IAnswer {
    id: string;
    choiceId: string;
    quizSessionId: string;
    isCorrect: boolean | null;
}
