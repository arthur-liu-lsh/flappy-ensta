[System.Serializable]
public class ScorePostModel
{
    public int userscore;
    public string checksum;

    public ScorePostModel(int newUserscore, string newChecksum) {
        userscore = newUserscore;
        checksum = newChecksum;
    }
}
