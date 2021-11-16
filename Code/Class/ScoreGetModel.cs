[System.Serializable]
public class ScoreGetModel
{
    public int highscore;
    public int userscore;

    public ScoreGetModel(int newHighscore, int newUserscore) {
        highscore = newHighscore;
        userscore = newUserscore;
    }
}
