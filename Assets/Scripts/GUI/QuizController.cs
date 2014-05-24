using UnityEngine;
using System.Collections;

public class QuizController : MonoBehaviour {
	public static QuizController instance;

	public int questionCount = 10;
	public int prizeGold = 100;
	[HideInInspector]
	public string category;
	
	private int answeredQuestionCount = 0;
	private Question question;
	private QuizAnswerButton[] choiceButtons = new QuizAnswerButton[4];
	private tk2dTextMesh t_header;
	private tk2dTextMesh t_query;
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		Transform choicesContainer = transform.Find("ChoicesContainer");
		int i = 0;
		foreach(Transform choiceTransform in choicesContainer) {
			choiceButtons[i] = choiceTransform.GetComponent<QuizAnswerButton>();
			i++;
		}
		t_query = transform.Find("BG/QueryText").GetComponent<tk2dTextMesh>();
		t_header = transform.Find("BG/Header").GetComponent<tk2dTextMesh>();
	}
	
	public void startQuiz(string category) {
		answeredQuestionCount = 0;
		this.category = category;
		setQuestion();
	}
	
	private void setQuestion() {
		Question newQuestion = XmlParse.instance.getRandomUnansweredQuestion(category);
		if (newQuestion == null) {
			noQuestionsLeft();
		} else {
			t_header.text = "Question " + (answeredQuestionCount + 1) + "/" + questionCount;
			t_header.Commit();
			question = newQuestion;
			t_query.text = question.query;
			t_query.Commit();
			FisherYatesShuffle.Shuffle(choiceButtons); // Shuffle choice order
			int i = 0;
			foreach(QuizAnswerButton choiceButton in choiceButtons) {
				choiceButton.setChoice(i, question.choices[i]);
				i++;
			}
		}
	}
	
	private void nextQuestion() {
		answeredQuestionCount++;
		if (answeredQuestionCount == questionCount) {
			pass();
		} else {
			setQuestion();
		}
	}
	
	private void pass() {
		GameSaveController.instance.getPlayer().gold += prizeGold;
		GameSaveController.instance.saveGame();
		TownController.instance.updateTexts();
		Notification.activate("You have completed the quiz successfully! This experience helped you to gain some gold!",
		                      () => exitQuiz());
	}
	
	private void fail() {
		Notification.activate("You have failed the quiz!",
		                      () => exitQuiz());
	}
	
	private void noQuestionsLeft() {
		Notification.activate("No questions left in the quiz!",
		                      () => exitQuiz());
	}
	
	private void exitQuiz() {
		TownMenu.instance.menuWindow();
	}
	
	public void answer(int choiceID) {
		if (choiceID == question.answer) {
			GameSaveController.instance.getPlayer().answeredQuestions.Add(question.id);
			GameSaveController.instance.getStats().countingStat("Questions answered", 1);
			GameSaveController.instance.saveGame();
			Notification.activate("Correct answer!",
			                      () => nextQuestion());
		} else {
			fail();
		}
	}
}
