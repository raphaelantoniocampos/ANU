using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// TODO: Scores na ultima fase
// TODO: Dicas de comandos início do jogo
// TODO: Alterar ventiladores teto
// TODO: Arrumar inimigos virando de um lado pro outro rapido
// TODO: Inimigos morrem com espinho e tomam stun com pancada na cabeça
// TODO: Volume slider
// TODO: Alterar skins
// TODO: Adicionar créditos
// TODO: Refatorar código inimigos
// TODO: Cutscenes
// TODO: Save
// TODO: Ranking

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Image[] lives;
    public int livesRemaining;
    public bool playerDie;
    public bool invulnerable;
    public float time;
    

    void Start()
    {
        instance = this;
    }
    
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1)
        {
            invulnerable = false;
        }
    }

    public void LoseLife()
    {
        if (!invulnerable)
        {
            StartCoroutine(FlashAfterDamage());
            invulnerable = true;
            time = 0;
            //diminiu o valor das vidas restantes
            livesRemaining--;
            FindObjectOfType<AudioManager>().Play("player_hit");

            //esconde uma imagem de coração
            lives[livesRemaining].enabled = false;

            //se ficar sem vida, da restart na fase
            if(livesRemaining <= 0)
            {
                Player.instance.Die();
                FindObjectOfType<AudioManager>().Play("player_die");
                DeathsAndTime.deathCount++;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FlashAfterDamage()
    {
        float flashDelay = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            Player.instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(flashDelay);
            Player.instance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(flashDelay);
        }
    }
}
