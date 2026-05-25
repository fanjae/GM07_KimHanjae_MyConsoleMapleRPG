# 콘솔 환경에서 구현한 턴제 RPG

<img width="620" height="420" alt="Image" src="https://github.com/user-attachments/assets/d3a8fde3-95b0-4fe6-902c-43c14ff03e5a"/><br>

> C# 환경에서 객체지향 구조를 기반으로 턴제 RPG의 전투, 맵 이동, 인벤토리, 상점, 저장 시스템 등 게임에서 사용되는 다양한 시스템을 구현하는 연습을 하기 위해 제작했습니다.
> Screen, View, System 계층을 분리하여 입력 처리, 화면 출력, 게임 로직의 책임을 나누는 것을 목표로 하였습니다.

## 프로젝트 개요

| 항목 | 내용 |
|---|---|
| 프로젝트명 | MyConsoleMapleRPG |
| 개발 기간 | 2026.05.13 ~ 2026.05.18 |
| 개발 인원 | 1명 |
| 개발 환경 | C#, .NET 8 |
| 실행 환경 | Windows Console |
| IDE | Visual Studio 2022 |
| 개발 기록 | https://fanjae.tistory.com/236 |

## 구현 기능

| 항목 | 화면 |  내용 |
|:---:|---|:---:|
| 전투 시스템 |<img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/d3a8fde3-95b0-4fe6-902c-43c14ff03e5a" /> | 턴 기반 전투, 기본 공격 및 스킬 공격 |
| 직업 선택 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/b0acfa0e-845e-433e-9d4d-661d855d6ba4" /> | 직업군을 2개 중 1개를 선택해 플레이 가능 |
| 성장 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/11eceb34-f500-449e-82a5-1ff2c861c5b2" /> | 경험치 획득 및 레벨업 처리 |
| 아이템 드랍 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/5125654d-0116-4cd5-b278-c2f35c20c32e" /> | 승리 할 때, 아이템 드랍 처리 |
| 상태 이상 시스템 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/ba720c96-04d2-4177-a362-8b1ff572166a" /> | 보스 몬스터 공격 시 상태 효과 적용 |
| 장비 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/cde343a4-f0e3-4da1-bedb-67cb03533352" /> | 장비 착용 및 능력치 반영 |
| 상점 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/b6385679-15ad-482f-8753-28a6c5efb2bb" /> | 아이템 구매 및 판매 |
| 저장 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/7c1c70e4-6969-4723-83b1-7fac8249791c" /> | JSON 기반 저장 및 불러오기 |
| 맵 이동 및 인카운터 전투 | <img width="420" height="300" alt="Image" src="https://github.com/user-attachments/assets/adb4cf63-8e2f-489a-b9c5-7bbee21ffb89" />  | 콘솔 환경에서 맵 이동 및 인카운터 전투 구현 |

## 구조 설계
콘솔 환경에서는 입력 처리, 화면 출력, 게임 로직이 하나의 클래스에 집중되기 쉽기 때문에
역할을 분리하여 유지보수성과 확장성을 고려한 구조로 구성했습니다.

## 조작 방법

| 키 | 기능 |
|:---:|---|
| ↑ ↓ ← → | 이동 및 메뉴 선택 |
| Z | 확인 |
| X | 스킬 선택 / 취소 |
| C | 전투 도망 |
| I | 인벤토리 열기 |
| S | 저장 |
| ESC | 닫기 및 종료 |

## 플레이 영상
[![플레이 영상](https://img.youtube.com/vi/cmpc88eea0M/maxresdefault.jpg)](https://youtu.be/cmpc88eea0M)

## 개발 기록
[My Turn Based Console RPG Project 개발 과정 정리](https://fanjae.tistory.com/236)
