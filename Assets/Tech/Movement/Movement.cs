﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  public SpriteRenderer charSpriteRenderer;
  public float walkAnimSpeed = 1;
  public List<Sprite> spritesUp = new List<Sprite>();
  public List<Sprite> spritesDown = new List<Sprite>();
  public List<Sprite> spritesHorizontal = new List<Sprite>();

  private List<Sprite> currentAnimation;
  private int currentSpriteIndex;
  private float walkAnimTime;

  private Vector2 movementDirection;
  [SerializeField]
  private float movementSpeed = 5f;

  // Start is called before the first frame update
  void Start() {
    currentSpriteIndex = 0;
    currentAnimation = spritesDown;
  }

  // Update is called once per frame
  void Update() {
    Move();
    if (movementDirection.magnitude > 0.0f) {
      UpdateAnimation();
    }
    PlayAnimation();
  }

  public void SetMovementVector(Vector2 movement) {
    movementDirection = movement;
  }

  private void Move() {
    transform.position = transform.position + ((Vector3)movementDirection * (movementSpeed * Time.deltaTime));
  }

  private void UpdateAnimation() {
    if (Mathf.Abs(movementDirection.y) > Mathf.Abs(movementDirection.x)) {
      if (movementDirection.y > 0)
        currentAnimation = spritesUp;
      else
        currentAnimation = spritesDown;
    } else {
      if (movementDirection.x < 0) {
        charSpriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        currentAnimation = spritesHorizontal;
      } else {
        charSpriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
        currentAnimation = spritesHorizontal;
      }
    }
  }

  private void PlayAnimation() {
    walkAnimTime += Time.deltaTime * walkAnimSpeed;
    if (walkAnimTime >= 1) {
      walkAnimTime = 0;
      currentSpriteIndex++;
    }

    if (currentSpriteIndex >= currentAnimation.Count || movementDirection.magnitude == 0)
      currentSpriteIndex = 0;

    charSpriteRenderer.sprite = currentAnimation[currentSpriteIndex];
  }
}